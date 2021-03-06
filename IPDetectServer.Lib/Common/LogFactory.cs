﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace IPDetectServer.Lib
{
    public static class LogFactory
    {
        /// <summary>
        /// Retrieves or creates a named logger.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Retrieves a logger named as the <paramref name="name"/>
        /// parameter. If the named logger already exists, then the
        /// existing instance will be returned. Otherwise, a new instance is
        /// created.
        /// </para>
        /// <para>By default, loggers do not have a set level but inherit
        /// it from the hierarchy. This is one of the central features of
        /// log4net.
        /// </para>
        /// </remarks>
        /// <param name="name">The name of the logger to retrieve.</param>
        /// <returns>The logger with the name specified.</returns>
        public static ILog GetLogger(string name)
        {
            return LogManager.GetLogger(name);
        }

        /// <summary>
        /// Shorthand for <see cref="LogFactory.GetLogger(string)"/>.
        /// </summary>
        /// <remarks>
        /// Get the logger for the fully qualified name of the type specified.
        /// </remarks>
        /// <param name="type">The full name of <paramref name="type"/> will be used as the name of the logger to retrieve.</param>
        /// <returns>The logger with the name specified.</returns>
        public static ILog GetLogger(Type type)
        {
            return LogManager.GetLogger(type);
        }
    }
}
