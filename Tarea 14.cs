/*
SELECT account_id, account_type, owner_name
FROM accounts 
WHERE is_active = TRUE
AND NOT EXISTS ( SELECT 1 FROM events WHERE events_account_id = accounts_account_id
AND events_type = 'deposit');
 
SELECT COUNT (*) AS total_activas_sin_depositos
FROM accounts 
WHERE is_active = TRUE
AND NOT EXISTS ( SELECT 1 FROM events WHERE events_account_id = accounts_account_id
AND events_type = 'deposit');


CORRECCIÓN: Viendo la solución de la tarea, se observa que la consulta de la actividad es correcta y que la técnica
de ausencia de eventos se aplica correctamente. 
La consulta final es más eficiente y clara al utilizar un LEFT JOIN en lugar de NOT EXISTS, 
lo que permite identificar cuentas activas sin depósitos de manera más directa y eficiente. Además, se mantiene la comparación
de pares de movimientos como evidencia de la ausencia de depósitos.

SELECT a.account_number, a.account_type
FROM core.accounts a
LEFT JOIN core.transactions t ON t.account_id = a.account_id AND t.tx_type = 'DEPOSIT'
WHERE a.is_active AND t.transaction_id IS NULL
ORDER BY a.account_number;
Lectura de la solución: El catálogo de productos solicita una lista más operativa: cuentas activas que aún no registran un depósito. La actividad ya estableció cómo expresar ausencia; ahora la tarea aplica esa técnica a un tipo de evento concreto y conserva la comparación de pares de movimientos como evidencia.
 * 