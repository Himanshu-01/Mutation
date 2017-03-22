using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Global
{
    public class EventHandler
    {
        // Output handler
        public delegate void OutputMessage_Handler(string Message);
        public static event OutputMessage_Handler OutputMessage_Event;

        // Open tag handler
        public delegate void OpenTag_Handler(string TagPath);
        public static event OpenTag_Handler OpenTag_Event;

        // Set clipboard data
        public delegate void SetCopyData_Handler(CopyStruct CopyData);
        public static event SetCopyData_Handler SetCopyData_Event;

        // Get clipboard data
        public delegate CopyStruct GetCopyData_Handler();
        public static event GetCopyData_Handler GetCopyData_Event;

        public static void OutputMessage(string Message)
        {
            if (OutputMessage_Event != null)
                OutputMessage_Event(Message);
        }

        public static void OpenTag(string TagPath)
        {
            if (OpenTag_Event != null)
                OpenTag_Event(TagPath);
        }

        public static CopyStruct GetCopyData()
        {
            if (GetCopyData_Event != null)
                return GetCopyData_Event();

            return new CopyStruct();
        }

        public static void SetCopyData(CopyStruct copydata)
        {
            if (SetCopyData_Event != null)
                SetCopyData_Event(copydata);
        }
    }
}
