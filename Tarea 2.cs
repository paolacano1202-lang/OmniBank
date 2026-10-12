/*
 CLIENTES:
id_cliente (UUID)
nombre (VARCHAR)
email (VARCHAR)

CUENTAS:
id_cuenta (UUID)
id_cliente (FK)
tipo_cuenta (VARCHAR)
saldo (DECIMAL)

TRANSACCIONES:
id_transaccion (UUID)
id_cuenta_origen (UUID)
id_cuenta_destino (UUID)
monto (DECIMAL)

CLIENTE |---------------< CUENTAS
CUENTA  |---------------< TRANSACCIONES >--------------> CUENTAS


CORRECCIÓN: Pude notar que en la relación entre CUENTAS y TRANSACCIONES, la relación es de uno a muchos, ya que una cuenta 
puede tener muchas transacciones asociadas. Además, cada transacción tiene una cuenta de origen y una cuenta de destino, 
lo que significa que hay dos relaciones entre CUENTAS y TRANSACCIONES.

[Clientes] 1 ------------ N [Cuentas]
- cliente_id (PK, UUID)       - cuenta_id (PK, UUID)
- nombre                      - cliente_id (FK, UUID)
- correo                      - moneda
- telefono                    - tipo_cuenta
- fecha_nacimiento            - saldo_actual (CHECK: >= 0)
- tax_id (UNIQUE)             - limite_credito
- is_active (Soft Delete)     - updated_at (Timestamp NFR)

                   (Cuenta Origen)
[Cuentas] 1 ------------------------- N [Transacciones]
                                          - transaccion_id (PK, UUID)
                   (Cuenta Destino)       - from_account (FK, UUID -> Cuentas)
[Cuentas] 1 ------------------------- N   - to_account (FK, UUID -> Cuentas)
                                          - monto
                                          - tipo_transaccion
                                          - fecha_completada
* 