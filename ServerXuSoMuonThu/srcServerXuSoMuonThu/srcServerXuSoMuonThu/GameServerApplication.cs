using ExitGames.Logging;
using ExitGames.Logging.Log4Net;
using log4net.Config;
using Photon.SocketServer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace srcServerXuSoMuonThu
{
    public class GameServerApplication : ApplicationBase
    {
        private readonly ILogger Log = LogManager.GetCurrentClassLogger();
        World world = new World();
        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            return new User(initRequest);
        }

        protected override void Setup()
        {
            var file = new FileInfo(Path.Combine(BinaryPath, "log4net.config"));
            if (file.Exists)
            {
                LogManager.SetLoggerFactory(Log4NetLoggerFactory.Instance);
                XmlConfigurator.ConfigureAndWatch(file);
            }
            world.Init();
            int workerThreads;
            int completePortThreads;
            ThreadPool.SetMaxThreads(31721, 31721);
            ThreadPool.GetMaxThreads(out workerThreads, out completePortThreads);
            Log.Debug("Server is Ready! " + workerThreads + " " + completePortThreads);
        }

        protected override void TearDown()
        {
            Log.Debug("Server was Stoped!");
        }
    }
}
