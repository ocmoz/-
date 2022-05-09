using System;
using System.Collections.Generic;
using System.Text;
using log4net;

namespace WebUtility
{
    internal class FileLogWriter
    {
        private readonly static FileLogWriter instance = new FileLogWriter();

        private ILog log = null;

        private FileLogWriter()
        {
            log4net.Config.XmlConfigurator.Configure();
            log = LogManager.GetLogger("FileLogWriter");
        }

        public static FileLogWriter Instance
        {
            get { return instance; }
        }

        public void WriteInfo(string msg)
        {
            string fromPage = null;
            try
            {
                fromPage = Common.GetScriptUrl;
            }
            catch
            {
            }
            if(!string.IsNullOrEmpty(fromPage))
                log.Info(string.Format("[{0}] {1}",fromPage,msg));
            else log.Info(msg);
        }

        public void WriteError(string msg)
        {
            string fromPage = null;
            try
            {
                fromPage = Common.GetScriptUrl;
            }
            catch
            {
            }
            if (!string.IsNullOrEmpty(fromPage))
                log.Error(string.Format("[{0}] {1}", fromPage, msg));
            else log.Error(msg);
        }

        public void WriteFatal(string msg)
        {
            string fromPage = null;
            try
            {
                fromPage = Common.GetScriptUrl;
            }
            catch
            {
            }
            if (!string.IsNullOrEmpty(fromPage))
                log.Fatal(string.Format("[{0}] {1}", fromPage, msg));
            else log.Fatal(msg);
        }

        public void WriteDebug(string msg)
        {
            string fromPage = null;
            try
            {
                fromPage = Common.GetScriptUrl;
            }
            catch
            {
            }
            if (!string.IsNullOrEmpty(fromPage))
                log.Debug(string.Format("[{0}] {1}", fromPage, msg));
            else log.Debug(msg);
        }

        public void WriteWarn(string msg)
        {
            string fromPage = null;
            try
            {
                fromPage = Common.GetScriptUrl;
            }
            catch
            {
            }
            if (!string.IsNullOrEmpty(fromPage))
                log.Warn(string.Format("[{0}] {1}", fromPage, msg));
            else log.Warn(msg);
        }
    }
}
