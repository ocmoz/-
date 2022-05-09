using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.SessionState;
namespace WebUtility
{
    public static class MockHttpContext
    {
        private const string ContextKeyAspSession = "AspSession";
        private static HttpContext context = null;
        public static void Init()
        {
            MySessionState myState = new MySessionState(Guid.NewGuid().ToString("N"),
            new SessionStateItemCollection(), new HttpStaticObjectsCollection(),
            5, true, HttpCookieMode.UseUri, SessionStateMode.InProc, false);

            TextWriter tw = new StringWriter();
            // 这个地方是可以修改的，这是设置的Web路径的地方，但文件是可以不存在的
            //HttpWorkerRequest wr = new SimpleWorkerRequest("Default.aspx", null, tw);
            context = HttpContext.Current;
            HttpSessionState state = Activator.CreateInstance(
            typeof(HttpSessionState),
            BindingFlags.Public | BindingFlags.NonPublic |
            BindingFlags.Instance | BindingFlags.CreateInstance,
            null,
            new object[] { myState },
            CultureInfo.CurrentCulture) as HttpSessionState;
            context.Items[ContextKeyAspSession] = state;
            HttpContext.Current = context;
        }
        public static HttpContext Context
        {
            get
            {
                return context;
            }
        }


    }
}