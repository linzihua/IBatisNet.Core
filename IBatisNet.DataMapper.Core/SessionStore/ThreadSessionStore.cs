using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBatisNet.DataMapper.SessionStore
{
    public class ThreadSessionStore : AbstractSessionStore
    {
        [ThreadStatic]
        private static Dictionary<string, ISqlMapSession> StaticSessions =
            new Dictionary<string, ISqlMapSession>();

        /// <summary>
        /// Initializes a new instance of the <see cref="CallContextSessionStore"/> class.
        /// </summary>
        /// <param name="sqlMapperId">The SQL mapper id.</param>
        public ThreadSessionStore(string sqlMapperId)
            : base(sqlMapperId)
        {
        }

        /// <summary>
        /// Get the local session
        /// </summary>
        public override ISqlMapSession LocalSession
        {
            get
            {
                if (StaticSessions == null)
                    StaticSessions = new Dictionary<string, ISqlMapSession>();

                ISqlMapSession result;
                StaticSessions.TryGetValue(sessionName, out result);
                return result;
                //             return StaticSessions[sessionName] as SqlMapSession;
            }
        }

        /// <summary>
        /// Store the specified session.
        /// </summary>
        /// <param name="session">The session to store</param>
        public override void Store(ISqlMapSession session)
        {
            StaticSessions[sessionName] = session;
        }

        /// <summary>
        /// Remove the local session.
        /// </summary>
        public override void Dispose()
        {
            StaticSessions[sessionName] = null;
        }
    }
}
