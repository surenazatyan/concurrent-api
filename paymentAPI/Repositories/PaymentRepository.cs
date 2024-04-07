using paymentAPI.Core.Types;
using paymentAPI.Repositories.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace paymentAPI.Repositories;

public class PaymentRepository
{
    private readonly ConcurrentDictionary<string, List<TransactionDTO>> _clientPayments = new ConcurrentDictionary<string, List<TransactionDTO>>();
    private readonly ConcurrentDictionary<string, SemaphoreSlim> _clientLocks = new ConcurrentDictionary<string, SemaphoreSlim>();

    public async Task<string> InitiatePaymentAsync(string clientId, TransactionDTO transactionDto)
    {
        var clientLock = _clientLocks.GetOrAdd(clientId, new SemaphoreSlim(1, 1));

        // max one thread per client ID
        if (!await clientLock.WaitAsync(0))
        { return null; }


        try
        {
            await Task.Delay(10000);

            var clientTransactions = _clientPayments.GetOrAdd(clientId, new List<TransactionDTO>());
            transactionDto.paymentId = Guid.NewGuid().ToString();
            clientTransactions.Add(transactionDto);
        }
        finally
        {
            clientLock.Release();
        }

        return transactionDto.paymentId;
    }

    public List<TransactionDTO> GetTransactions(string iban)
    {
        var filteredTransactions = new List<TransactionDTO>();

        foreach (var clientTransactions in _clientPayments.Values)
        {
            filteredTransactions.AddRange(
            clientTransactions
                ?.Where(t => t.CreditorAccount.Equals(iban) || t.DebtorAccount.Equals(iban))
                .ToList());
        }

        return filteredTransactions;
    }
}
