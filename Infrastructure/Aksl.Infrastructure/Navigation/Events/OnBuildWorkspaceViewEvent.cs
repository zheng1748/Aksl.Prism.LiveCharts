using Prism.Events;

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
        public MenuItem CurrentMenuItem { get; set; }
        #endregion
    }

    public class OnBuildHamburgerMenuSideBarWorkspaceViewEvent : OnBuildWorkspaceViewEvent<OnBuildHamburgerMenuSideBarWorkspaceViewEvent>
    {
        #region Constructors
        public OnBuildHamburgerMenuSideBarWorkspaceViewEvent()
        {
        }
        #endregion
    }

    public class OnBuildHamburgerMenuTreeSideBarWorkspaceViewEvent : OnBuildWorkspaceViewEvent<OnBuildHamburgerMenuTreeSideBarWorkspaceViewEvent>
    {
        #region Constructors
        public OnBuildHamburgerMenuTreeSideBarWorkspaceViewEvent()
        {
        }
        #endregion
    }

    public class OnBuildHamburgerMenuNavigationSideBarWorkspaceViewEvent : OnBuildWorkspaceViewEvent<OnBuildHamburgerMenuNavigationSideBarWorkspaceViewEvent>
    {
        #region Constructors
        public OnBuildHamburgerMenuNavigationSideBarWorkspaceViewEvent()
        {
        }
        #endregion
    }

    public class OnBuildHamburgerMenuWorkspaceViewEvent : OnBuildWorkspaceViewEvent<OnBuildHamburgerMenuWorkspaceViewEvent>
    {
        #region Constructors
        public OnBuildHamburgerMenuWorkspaceViewEvent()
        {
        }
        #endregion
    }

    public class OnBuildHamburgerMenuTreeBarWorkspaceViewEvent : OnBuildWorkspaceViewEvent<OnBuildHamburgerMenuTreeBarWorkspaceViewEvent>
    {
        #region Constructors
        public OnBuildHamburgerMenuTreeBarWorkspaceViewEvent()
        {
        }
        #endregion
    }

    public class OnBuildHamburgerMenuNavigationBarWorkspaceViewEvent : OnBuildWorkspaceViewEvent<OnBuildHamburgerMenuNavigationBarWorkspaceViewEvent>
    {
        #region Constructors
        public OnBuildHamburgerMenuNavigationBarWorkspaceViewEvent()
        {
        }
        #endregion
    }
}