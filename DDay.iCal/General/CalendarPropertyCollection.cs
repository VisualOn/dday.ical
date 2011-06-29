﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Runtime.Serialization;
using DDay.Collections;

namespace DDay.iCal
{
#if !SILVERLIGHT
    [Serializable]
#endif
    public class CalendarPropertyCollection :
        GroupedValueCollection<string, ICalendarProperty, object>,
        ICalendarPropertyCollection
    {
        #region Private Fields

        ICalendarObject m_Parent;
        bool m_CaseInsensitive;

        #endregion

        #region Constructors

        public CalendarPropertyCollection()
        {
        }

        public CalendarPropertyCollection(ICalendarObject parent, bool caseInsensitive)
        {
            m_Parent = parent;
            m_CaseInsensitive = caseInsensitive;

            ItemAdded += new EventHandler<ObjectEventArgs<ICalendarProperty>>(CalendarPropertyList_ItemAdded);
            ItemRemoved += new EventHandler<ObjectEventArgs<ICalendarProperty>>(CalendarPropertyList_ItemRemoved);
        }

        #endregion

        #region Overrides

        protected override string KeyModifier(string key)
        {
            if (m_CaseInsensitive)
                return key.ToUpper();
            return key;
        }

        #endregion

        #region Event Handlers

        void CalendarPropertyList_ItemRemoved(object sender, ObjectEventArgs<ICalendarProperty> e)
        {
            e.Object.Parent = null;
        }

        void CalendarPropertyList_ItemAdded(object sender, ObjectEventArgs<ICalendarProperty> e)
        {
            e.Object.Parent = m_Parent;
        }

        #endregion
    }
}