using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Foundation.Common
{
    public class LogHandlers : ILogHandler
    {
        private HashSet<ILogHandler> _handlers = new HashSet<ILogHandler>();

        public LogHandlers(params ILogHandler[] handlers)
        {
            this.AddHandlers(handlers);
        }

        public void Clear()
        {
            _handlers.Clear();
        }

        public void AddHandlers(params ILogHandler[] handlers)
        {
            if (handlers == null || handlers.Length == 0)
            {
                return;
            }

            for (var i = 0; i < handlers.Length; i++)
            {
                this.AddHandler(handlers[i]);
            }
        }

        public void AddHandler(ILogHandler handler)
        {
            if (handler == null)
            {
                return;
            }

            if (_handlers.Contains(handler))
            {
                return;
            }

            _handlers.Add(handler);
        }

        public void RemoveHandler(ILogHandler handler)
        {
            if (handler == null)
            {
                return;
            }

            if (!_handlers.Contains(handler))
            {
                return;
            }

            _handlers.Remove(handler);

        }

        public void LogException(Exception exception, UnityEngine.Object context)
        {
            foreach (var item in _handlers)
            {
                item.LogException(exception, context);
            }
        }

        public void LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
        {
            foreach (var item in _handlers)
            {
                item.LogFormat(logType, context, format, args);
            }
        }
    }
}


