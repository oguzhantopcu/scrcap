using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.PresentationFramework.ApplicationModel;
using EkranPaylas.ViewModels;

namespace EkranPaylas.Utilities
{
    public class StateHolder : IHandle<ScreenGrabberState>, IStateHolder
    {
        public StateHolder(IEventAggregator eventAggregator)
        {
            eventAggregator.Subscribe(this);
        }

        public ScreenGrabberState State { get; protected set; }

        public void Handle(ScreenGrabberState message)
        {
            this.State = message;
        }
    }
}
