using Photon.SocketServer;

namespace srcServerXuSoMuonThu.Handler
{
    // tạo xong handler nhớ có lệnh bắt ở world
    public interface BaseHandler
    {
        bool OnHandlerRequest(OperationRequest request, SendParameters sendData, User user);
    }
}