using paymentAPI.Services.PaymentService;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System;
using System.Net;
using paymentAPI.ViewModels;
using System.Transactions;
using paymentAPI.Core.Types;

namespace paymentAPI.Controllers
{
    [ApiController]
    public class PaymentApiController : ControllerBase
    {
        IPaymentService paymentService;
        public PaymentApiController(IPaymentService paymentService)
        {
            this.paymentService = paymentService;
        }

        /// <summary>
        /// Initiation of a payment.
        /// The method should guarantee that max one payment initiation is processing at any given time for each Client-ID. 
        /// </summary>
        /// <response code="201">Returns Created status with unique Payment-ID.</response>
        /// <response code="409">Returns Conflict status if the method is called for a Client-ID that already has a payment processing.</response>
        [HttpPost]
        [Route("/payment")]
        public async Task<IActionResult> InitiatePayment(
            [Required(ErrorMessage = "\"Client-ID is required\"")]
            [FromHeader(Name = "Client-ID")]
            string clientId,
            [FromBody]
            AddTransactionRequestModel transaction)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var newPaymentId = await paymentService.AddTransaction(clientId, transaction);

            if (newPaymentId != null)
            {
                return new ObjectResult(new { PaymentId = newPaymentId })
                {
                    StatusCode = (int)HttpStatusCode.Created
                };
            }
            else
            {
                return Problem(statusCode: 409, detail: $"payment was already made, is progress for Client-ID {clientId}");
            }
        }


        /// <summary>
        /// This method returns transactions (payments) for a given account number (iban) 
        /// as soon as they are completed(e.g.should include all payments initiated more 
        /// than two seconds ago on the account). 
        /// If no payment has been completed(e.g.less than two seconds after initiation), 
        /// the method should return HTTP status code “No Content”.
        /// </summary>
        /// <param name="iban">IBAN (String up to 34 alphanumeric characters).</param>
        /// <response code="200">Aggregated credit data for given ssn.</response>
        /// <response code="204">No Content.</response>
        [HttpGet]
        [Route("/accounts/{iban}/transactions")]
        public virtual async Task<IActionResult> GetTransactions(
            [FromRoute]
            [Required]
            [StringLength(34, ErrorMessage = "Incorrect value for IBAN, should be 34 alphanumeric characters.")]
            string iban)
        {
            var transactionsResponse = paymentService.GetTransactions(iban);

            if (transactionsResponse.Count == 0)
                return NoContent();
            else return Ok(transactionsResponse);
        }
    }
}
