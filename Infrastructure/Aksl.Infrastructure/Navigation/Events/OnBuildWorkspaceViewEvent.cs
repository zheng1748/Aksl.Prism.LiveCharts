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

    public class OnBuildIndustryManagerHamburgerMenuWorkspaceViewEvent : OnBuildWorkspaceViewEventbase
    {
        #region Constructors
        public OnBuildIndustryManagerHamburgerMenuWorkspaceViewEvent()
        {
            Name =nameof(OnBuildIndustryManagerHamburgerMenuWorkspaceViewEvent);
        }
        #endregion
    }

    public class OnBuildCustomerManagerHamburgerMenuWorkspaceViewEvent : OnBuildWorkspaceViewEventbase
    {
        #region Constructors
        public OnBuildCustomerManagerHamburgerMenuWorkspaceViewEvent()
        {
            Name = nameof(OnBuildCustomerManagerHamburgerMenuWorkspaceViewEvent);
        }
        #endregion
    }

    public class OnBuildIndustryManagerHamburgerNavigationBarWorkspaceViewEvent : OnBuildWorkspaceViewEventbase
    {
        #region Constructors
        public OnBuildIndustryManagerHamburgerNavigationBarWorkspaceViewEvent()
        {
            Name = nameof(OnBuildIndustryManagerHamburgerMenuWorkspaceViewEvent);
        }
        #endregion
    }

    public class OnBuildCustomerManagerHamburgerNavigationBarWorkspaceViewEvent : OnBuildWorkspaceViewEventbase
    {
        #region Constructors
        public OnBuildCustomerManagerHamburgerNavigationBarWorkspaceViewEvent()
        {
            Name = nameof(OnBuildCustomerManagerHamburgerMenuWorkspaceViewEvent);
        }
        #endregion
    }

    public class OnBuildIndustryManagerHamburgerTreeBarWorkspaceViewEvent : OnBuildWorkspaceViewEventbase
    {
        #region Constructors
        public OnBuildIndustryManagerHamburgerTreeBarWorkspaceViewEvent()
        {
            Name = nameof(OnBuildIndustryManagerHamburgerMenuWorkspaceViewEvent);
        }
        #endregion
    }

    public class OnBuildCustomerManagerHamburgerTreeBarWorkspaceViewEvent : OnBuildWorkspaceViewEventbase
    {
        #region Constructors
        public OnBuildCustomerManagerHamburgerTreeBarWorkspaceViewEvent()
        {
            Name = nameof(OnBuildCustomerManagerHamburgerMenuWorkspaceViewEvent);
        }
        #endregion
    }

    //public class OnBuildHamburgerMenuTreeBarWorkspaceViewEvent : OnBuildWorkspaceViewEventbase
    //{
    //    #region Constructors
    //    public OnBuildHamburgerMenuTreeBarWorkspaceViewEvent()
    //    {
    //        Name = typeof(OnBuildHamburgerMenuTreeBarWorkspaceViewEvent).Name;
    //    }
    //    #endregion
    //}

    public class OnBuildHamburgerMenuNavigationBarWorkspaceViewEvent : OnBuildWorkspaceViewEventbase
    {
        #region Constructors
        public OnBuildHamburgerMenuNavigationBarWorkspaceViewEvent()
        {
            Name = typeof(OnBuildHamburgerMenuNavigationBarWorkspaceViewEvent).Name;
        }
        #endregion
    }

    //public class OnBuildIndustryManagerWorkspaceViewEvent : OnBuildWorkspaceViewEventbase
    //{
    //    #region Constructors
    //    public OnBuildIndustryManagerWorkspaceViewEvent()
    //    {
    //        Name = typeof(OnBuildIndustryManagerWorkspaceViewEvent).Name;
    //    }
    //    #endregion
    //}

    //public class OnBuildCustomerManagerWorkspaceViewEvent : OnBuildWorkspaceViewEventbase
    //{
    //    #region Constructors
    //    public OnBuildCustomerManagerWorkspaceViewEvent()
    //    {
    //        Name = typeof(OnBuildCustomerManagerWorkspaceViewEvent).Name;
    //    }
    //    #endregion
    //}

}