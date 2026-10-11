/*
WITH account_metrics AS (
SELECT
a.account_id,
a.currency,
COALESCE (SUM (t.amount), 0) AS balance,
COUNT (t.transaction_id) AS num_transactions
FROM accounts a
LEFT JOIN transactions t
ON a.account_id = t.account_id
GROUP BY a.account_id, a.currency
)
SELECT
currency,
COUNT (account_id) AS total_accounts,
SUM (total_amount) AS gross_volume,
ROUND (AVG (balance), 2) AS avg_balance,
ROUND (AVG (num_transactions), 2) AS avg_transactions
FROM account_metrics
GROUP BY currency;

CORRECCIÓN:
Esta vez aprendí a usar una subconsulta en la cláusula FROM. Mi tarea estaba bien, pero no era la solución correcta.
Las correcciones que señaló el profesor fueron:
1. No se debe reescribir el cálculo de movimientos, sino usarlo como una subconsulta en la cláusula FROM.
2. No perder cuentas activas de volumen cero.
También aprendí a unsar el AVG en lugar de la media.


WITH movement AS (
 SELECT account_id, SUM(amount) AS gross_volume
 FROM core.transactions GROUP BY account_id
), account_metrics AS (
 SELECT a.account_id, a.currency, COALESCE(m.gross_volume,0) AS gross_volume
 FROM core.accounts a LEFT JOIN movement m USING (account_id)
 WHERE a.is_active
), by_currency AS (
 SELECT currency, COUNT(*) AS account_count, SUM(gross_volume) AS gross_volume
 FROM account_metrics GROUP BY currency
)
SELECT currency, account_count, gross_volume,
       ROUND(gross_volume / account_count,2) AS average_per_account
FROM by_currency ORDER BY currency;
SELECT (SELECT COUNT(*) FROM core.accounts WHERE is_active) AS active_accounts,
       (SELECT COALESCE(SUM(amount),0) FROM core.transactions) AS direct_gross_volume,
       (SELECT COUNT(*) FROM core.transactions t JOIN core.accounts a USING (account_id)
        WHERE a.account_number='ACCT-3002') AS empty_account_events;
Lectura de la solución: Finanzas quiere una vista por moneda a partir del mismo pipeline, sin reescribir el cálculo de movimientos ni perder cuentas activas de volumen cero. La tarea añade una etapa de agregación sobre el resultado por cuenta; ese cambio de grano debe quedar explícito en nombres y columnas.
 * 