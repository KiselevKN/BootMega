using System;
using System.Collections.Generic;
using BootMega.Interaction.Events;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Module.Settings
{
    public class ExceptionInterceptor : IInterceptionBehavior
    {       
        #region fields

        private readonly IEventAggregator eventAggregator;
        private readonly ILoggerFacade logger;

        #endregion

        #region ctors

        public ExceptionInterceptor(IEventAggregator eventAggregator, ILoggerFacade logger)
        {
            this.eventAggregator = eventAggregator;
            this.logger = logger;
        }

        #endregion
        
        #region IInterceptionBehavior Members

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            var result = getNext()(input, getNext);

            if (result.Exception != null)
            {               
                eventAggregator.GetEvent<SettingsExceptionEvent>().Publish(result.Exception);
                logger.Log(result.Exception.ToString(), Category.Exception, Priority.None);
                result.Exception = null;
            }
            return result;
        }

        public bool WillExecute
        {
            get { return true; }
        }

        #endregion
    }
}
