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
}