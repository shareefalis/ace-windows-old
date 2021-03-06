﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using VATRP.LinphoneWrapper.Enums;

namespace VATRP.LinphoneWrapper
{
    public static partial class LinphoneAPI
    {

        /**
 * Get the mime type of the content data.
 * @param[in] content LinphoneContent object.
 * @return The mime type of the content data, for example "application".
 */

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr linphone_content_get_type(IntPtr content);

        /**
         * Set the mime type of the content data.
         * @param[in] content LinphoneContent object.
         * @param[in] type The mime type of the content data, for example "application".
         */

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void linphone_content_set_type(IntPtr content, IntPtr type);

        /**
         * Get the mime subtype of the content data.
         * @param[in] content LinphoneContent object.
         * @return The mime subtype of the content data, for example "html".
         */

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr linphone_content_get_subtype(IntPtr content);

        /**
         * Set the mime subtype of the content data.
         * @param[in] content LinphoneContent object.
         * @param[in] subtype The mime subtype of the content data, for example "html".
         */

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void linphone_content_set_subtype(IntPtr content, IntPtr subtype);

        /**
         * Get the content data buffer, usually a string.
         * @param[in] content LinphoneContent object.
         * @return The content data buffer.
         */

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr linphone_content_get_buffer(IntPtr content);

        /**
         * Set the content data buffer, usually a string.
         * @param[in] content LinphoneContent object.
         * @param[in] buffer The content data buffer.
         * @param[in] size The size of the content data buffer.
         */

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void linphone_content_set_buffer(IntPtr content, IntPtr buffer, uint size);

        /**
         * Get the string content data buffer.
         * @param[in] content LinphoneContent object
         * @return The string content data buffer.
         */

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr linphone_content_get_string_buffer(IntPtr content);

        /**
         * Set the string content data buffer.
         * @param[in] content LinphoneContent object.
         * @param[in] buffer The string content data buffer.
         */

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void linphone_content_set_string_buffer(IntPtr content, IntPtr buffer);

        /**
         * Get the content data buffer size, excluding null character despite null character is always set for convenience.
         * @param[in] content LinphoneContent object.
         * @return The content data buffer size.
         */

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint linphone_content_get_size(IntPtr content);

        /**
         * Set the content data size, excluding null character despite null character is always set for convenience.
         * @param[in] content LinphoneContent object
         * @param[in] size The content data buffer size.
         */

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void linphone_content_set_size(IntPtr content, uint size);

        /**
         * Get the encoding of the data buffer, for example "gzip".
         * @param[in] content LinphoneContent object.
         * @return The encoding of the data buffer.
         */

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr linphone_content_get_encoding(IntPtr content);

        /**
         * Set the encoding of the data buffer, for example "gzip".
         * @param[in] content LinphoneContent object.
         * @param[in] encoding The encoding of the data buffer.
         */

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void linphone_content_set_encoding(IntPtr content, IntPtr encoding);

        /**
         * Get the name associated with a RCS file transfer message. It is used to store the original filename of the file to be downloaded from server.
         * @param[in] content LinphoneContent object.
         * @return The name of the content.
         */

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr linphone_content_get_name(IntPtr content);

        /**
         * Set the name associated with a RCS file transfer message. It is used to store the original filename of the file to be downloaded from server.
         * @param[in] content LinphoneContent object.
         * @param[in] name The name of the content.
         */

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void linphone_content_set_name(IntPtr content, IntPtr name);

    }
}
