import repository.CuentaRepository;
import services.TransaccionService;
import sockets.SocketBancario;

public class Main {
    public static void main(String[] args) {
        CuentaRepository cuentaRepository = new CuentaRepository();
        TransaccionService transaccionService = new TransaccionService(cuentaRepository);
        SocketBancario socketBancario = new SocketBancario(transaccionService);

        socketBancario.start(); //inicia el Socket para Recibir Transacciones
    }
}