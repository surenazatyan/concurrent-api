test curls

curl --request GET \
  --url http://localhost:5000/accounts/DE89%203704%200044%200532%200130%2011/transactions



curl --request POST \
  --url http://localhost:5000/payment \
  --header 'Content-Type: application/json' \
  --header 'client-id: 1' \
  --data '{
    "debtor-account": "DE89 3704 0044 0532 0130 11",
    "creditor-account": "DE89 3704 0044 0532 0130 12",
    "Instructed-Amount": "100.00",
    "currency": "EUR"
}'


curl --request POST \
  --url http://localhost:5000/payment \
  --header 'Content-Type: application/json' \
  --header 'client-id: 1' \
  --data '{
    "debtor-account": "DE89 3704 0044 0532 0130 21",
    "creditor-account": "DE89 3704 0044 0532 0130 22",
    "Instructed-Amount": "150.00",
    "currency": "USD"
}'


curl --request POST \
  --url http://localhost:5000/payment \
  --header 'Content-Type: application/json' \
  --header 'client-id: 2' \
  --data '{
    "debtor-account": "DE89 3704 0044 0532 0130 31",
    "creditor-account": "DE89 3704 0044 0532 0130 11",
    "Instructed-Amount": "200.00",
    "currency": "AUD"
}'


curl --request POST \
  --url http://localhost:5000/payment \
  --header 'Content-Type: application/json' \
  --header 'client-id: 2' \
  --data '{
    "debtor-account": "DE89 3704 0044 0532 0130 41",
    "creditor-account": "DE89 3704 0044 0532 0130 42",
    "Instructed-Amount": "250.00",
    "currency": "AMD"
}'
