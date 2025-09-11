
using Prism.Events;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Security.AccessControl;
using System.Xml.Linq;

namespace Aksl.Infrastructure.Events
{
    //[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    //public class WorkspaceViewEventNameAttribute : Attribute
    //{
    //    #region Constructors
    //    public WorkspaceViewEventNameAttribute(string workspaceViewEventName)
    //    {
    //        WorkspaceViewEventName = workspaceViewEventName;
    //    }
    //    #endregion

    //    #region Properties
    //    public string WorkspaceViewEventName { get; set; }
    //    #endregion
    //}

    //public static class CustomAttributeExtensions
    //{
    //    #region Methods
    //    public static TReturn GetCustomAttributeValue<TAttribute, TReturn>(this Type sourceType, Func<TAttribute, TReturn> attributeValueAction) where TAttribute : Attribute
    //    {
    //        return GetAttributeValue(sourceType, attributeValueAction, null);
    //    }

    //    public static TReturn GetCustomAttributeValue<TAttribute, TReturn>(this Type sourceType, Func<TAttribute, TReturn> attributeValueAction, string propertyName) where TAttribute : Attribute
    //    {
    //        return GetAttributeValue(sourceType, attributeValueAction, propertyName);
    //    }

    //    private static TReturn GetAttributeValue<TAttribute, TReturn>(this Type type, Func<TAttribute, TReturn> attributeValueAction, string name) where TAttribute : Attribute
    //    {
    //        TAttribute attribute = default;
    //        if (string.IsNullOrEmpty(name))
    //        {
    //            attribute = type.GetCustomAttribute<TAttribute>(false);
    //        }
    //        else
    //        {
    //            var propertyInfo = type.GetProperty(name);
    //            if (propertyInfo is not null)
    //            {
    //                attribute = propertyInfo.GetCustomAttribute<TAttribute>(false);
    //            }
    //            else
    //            {
    //                var fieldInfo = type.GetField(name);
    //                if (fieldInfo is not null)
    //                {
    //                    attribute = fieldInfo.GetCustomAttribute<TAttribute>(false);
    //                }
    //            }
    //        }

    //        return attribute == null ? default : attributeValueAction(attribute);
    //    }

    //    #endregion
    //}
    public interface IPubSubEventFactory<TPayload>
    {
        #region Properties
        public string Name { get; set; }
        #endregion

        #region Methods
        EventBase GetEventBase(string name);
        #endregion
    }

    //public class PubSubEventFactory : IPubSubEventFactory<EventBase>
    //{
    //}
}