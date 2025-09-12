using Prism.Events;
using System;
using System.Collections;

namespace Aksl.Infrastructure.Events
{
    public class OnBuildWorkspaceViewEvent<TPayload> : PubSubEvent<TPayload>
    {
        #region Constructors
        public OnBuildWorkspaceViewEvent()
        {
        }
        #endregion

        #region Properties
        public string Name { get; set; }

        public MenuItem CurrentMenuItem { get; set; }
        #endregion
    }

    public class OnBuildWorkspaceViewEventbase : PubSubEvent<OnBuildWorkspaceViewEventbase>,IEquatable<OnBuildWorkspaceViewEventbase>
    {
        #region Constructors
        public OnBuildWorkspaceViewEventbase()
        {
        }
        #endregion

        #region Properties
        public string Name { get; set; }

        public MenuItem CurrentMenuItem { get; set; }
        #endregion

        #region IEquatable Method
        public bool Equals(OnBuildWorkspaceViewEventbase other)
        {
            if(other is null)
            {
                return false;
            }

            if ( string.IsNullOrEmpty(other.Name))
            {
                return false;
            }


            return this.Name.Equals(other.Name,StringComparison.InvariantCultureIgnoreCase);
        }
        #endregion
    }

    public class OnBuildHamburgerMenuSideBarWorkspaceViewEvent : OnBuildWorkspaceViewEventbase
    {
        #region Constructors
        public OnBuildHamburgerMenuSideBarWorkspaceViewEvent()
        {
            Name = typeof(OnBuildHamburgerMenuSideBarWorkspaceViewEvent).Name;
        }
        #endregion
    }

    public class OnBuildHamburgerMenuTreeSideBarWorkspaceViewEvent : OnBuildWorkspaceViewEventbase
    {
        #region Constructors
        public OnBuildHamburgerMenuTreeSideBarWorkspaceViewEvent()
        {
            Name = typeof(OnBuildHamburgerMenuTreeSideBarWorkspaceViewEvent).Name;
        }
        #endregion
    }

    public class OnBuildHamburgerMenuNavigationSideBarWorkspaceViewEvent : OnBuildWorkspaceViewEventbase
    {
        #region Constructors
        public OnBuildHamburgerMenuNavigationSideBarWorkspaceViewEvent()
        {
            Name = typeof(OnBuildHamburgerMenuNavigationSideBarWorkspaceViewEvent).Name;
        }
        #endregion
    }

    public class OnBuildAxesManagerHamburgerMenuWorkspaceViewEvent : OnBuildWorkspaceViewEventbase
    {
        #region Constructors
        public OnBuildAxesManagerHamburgerMenuWorkspaceViewEvent()
        {
            Name = nameof(OnBuildAxesManagerHamburgerMenuWorkspaceViewEvent);
        }
        #endregion
    }

    public class OnBuildBarsManagerHamburgerMenuWorkspaceViewEvent : OnBuildWorkspaceViewEventbase
    {
        #region Constructors
        public OnBuildBarsManagerHamburgerMenuWorkspaceViewEvent()
        {
            Name = nameof(OnBuildBarsManagerHamburgerMenuWorkspaceViewEvent);
        }
        #endregion
    }

    public class OnBuildBoxManagerHamburgerMenuWorkspaceViewEvent : OnBuildWorkspaceViewEventbase
    {
        #region Constructors
        public OnBuildBoxManagerHamburgerMenuWorkspaceViewEvent()
        {
            Name = nameof(OnBuildBoxManagerHamburgerMenuWorkspaceViewEvent);
        }
        #endregion
    }

    public class OnBuildDesignManagerHamburgerMenuWorkspaceViewEvent : OnBuildWorkspaceViewEventbase
    {
        #region Constructors
        public OnBuildDesignManagerHamburgerMenuWorkspaceViewEvent()
        {
            Name = nameof(OnBuildDesignManagerHamburgerMenuWorkspaceViewEvent);
        }
        #endregion
    }

    public class OnBuildErrorManagerHamburgerMenuWorkspaceViewEvent : OnBuildWorkspaceViewEventbase
    {
        #region Constructors
        public OnBuildErrorManagerHamburgerMenuWorkspaceViewEvent()
        {
            Name = nameof(OnBuildErrorManagerHamburgerMenuWorkspaceViewEvent);
        }
        #endregion
    }

    public class OnBuildEventsManagerHamburgerMenuWorkspaceViewEvent : OnBuildWorkspaceViewEventbase
    {
        #region Constructors
        public OnBuildEventsManagerHamburgerMenuWorkspaceViewEvent()
        {
            Name = nameof(OnBuildEventsManagerHamburgerMenuWorkspaceViewEvent);
        }
        #endregion
    }

    public class OnBuildFinancialManagerHamburgerMenuWorkspaceViewEvent : OnBuildWorkspaceViewEventbase
    {
        #region Constructors
        public OnBuildFinancialManagerHamburgerMenuWorkspaceViewEvent()
        {
            Name = nameof(OnBuildFinancialManagerHamburgerMenuWorkspaceViewEvent);
        }
        #endregion
    }

    public class OnBuildGeneralManagerHamburgerMenuWorkspaceViewEvent: OnBuildWorkspaceViewEventbase
    {
        #region Constructors
        public OnBuildGeneralManagerHamburgerMenuWorkspaceViewEvent()
        {
            Name = nameof(OnBuildGeneralManagerHamburgerMenuWorkspaceViewEvent);
        }
        #endregion
    }

    public class OnBuildHeatManagerHamburgerMenuWorkspaceViewEvent : OnBuildWorkspaceViewEventbase
    {
        #region Constructors
        public OnBuildHeatManagerHamburgerMenuWorkspaceViewEvent()
        {
            Name = nameof(OnBuildHeatManagerHamburgerMenuWorkspaceViewEvent);
        }
        #endregion
    }

    public class OnBuildLinesManagerHamburgerMenuWorkspaceViewEvent : OnBuildWorkspaceViewEventbase
    {
        #region Constructors
        public OnBuildLinesManagerHamburgerMenuWorkspaceViewEvent()
        {
            Name = nameof(OnBuildLinesManagerHamburgerMenuWorkspaceViewEvent);
        }
        #endregion
    }

    public class OnBuildMapsManagerHamburgerMenuWorkspaceViewEvent : OnBuildWorkspaceViewEventbase
    {
        #region Constructors
        public OnBuildMapsManagerHamburgerMenuWorkspaceViewEvent()
        {
            Name = nameof(OnBuildMapsManagerHamburgerMenuWorkspaceViewEvent);
        }
        #endregion
    }

    public class OnBuildPiesManagerHamburgerMenuWorkspaceViewEvent : OnBuildWorkspaceViewEventbase
    {
        #region Constructors
        public OnBuildPiesManagerHamburgerMenuWorkspaceViewEvent()
        {
            Name = nameof(OnBuildPiesManagerHamburgerMenuWorkspaceViewEvent);
        }
        #endregion
    }


    public class OnBuildPolarManagerHamburgerMenuWorkspaceViewEvent : OnBuildWorkspaceViewEventbase
    {
        #region Constructors
        public OnBuildPolarManagerHamburgerMenuWorkspaceViewEvent()
        {
            Name = nameof(OnBuildPolarManagerHamburgerMenuWorkspaceViewEvent);
        }
        #endregion
    }

    public class OnBuildScatterManagerHamburgerMenuWorkspaceViewEvent : OnBuildWorkspaceViewEventbase
    {
        #region Constructors
        public OnBuildScatterManagerHamburgerMenuWorkspaceViewEvent()
        {
            Name = nameof(OnBuildScatterManagerHamburgerMenuWorkspaceViewEvent);
        }
        #endregion
    }

    public class OnBuildStackedAreaManagerHamburgerMenuWorkspaceViewEvent : OnBuildWorkspaceViewEventbase
    {
        #region Constructors
        public OnBuildStackedAreaManagerHamburgerMenuWorkspaceViewEvent()
        {
            Name = nameof(OnBuildStackedAreaManagerHamburgerMenuWorkspaceViewEvent);
        }
        #endregion
    }

    public class OnBuildStackedBarsManagerHamburgerMenuWorkspaceViewEvent : OnBuildWorkspaceViewEventbase
    {
        #region Constructors
        public OnBuildStackedBarsManagerHamburgerMenuWorkspaceViewEvent()
        {
            Name = nameof(OnBuildStackedBarsManagerHamburgerMenuWorkspaceViewEvent);
        }
        #endregion
    }

    public class OnBuildStepLinesManagerHamburgerMenuWorkspaceViewEvent : OnBuildWorkspaceViewEventbase
    {
        #region Constructors
        public OnBuildStepLinesManagerHamburgerMenuWorkspaceViewEvent()
        {
            Name = nameof(OnBuildStepLinesManagerHamburgerMenuWorkspaceViewEvent);
        }
        #endregion
    }

    public class OnBuildTestManagerHamburgerMenuWorkspaceViewEvent : OnBuildWorkspaceViewEventbase
    {
        #region Constructors
        public OnBuildTestManagerHamburgerMenuWorkspaceViewEvent()
        {
            Name = nameof(OnBuildTestManagerHamburgerMenuWorkspaceViewEvent);
        }
        #endregion
    }

    public class OnBuildVisualTestManagerHamburgerMenuWorkspaceViewEvent : OnBuildWorkspaceViewEventbase
    {
        #region Constructors
        public OnBuildVisualTestManagerHamburgerMenuWorkspaceViewEvent()
        {
            Name = nameof(OnBuildVisualTestManagerHamburgerMenuWorkspaceViewEvent);
        }
        #endregion
    }
}