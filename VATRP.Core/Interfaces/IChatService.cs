﻿using System;
using System.Collections.ObjectModel;
using com.vtcsecure.ace.windows.Model;
using VATRP.Core.Events;
using VATRP.Core.Model;

namespace VATRP.Core.Interfaces
{
    public interface IChatService : IVATRPservice
    {
        VATRPChat GetChat(VATRPContact contact);
        VATRPContact FindContact(ContactID contactID);
        ObservableCollection<VATRPContact> Contacts { get; }

        bool ComposeAndSendMessage(IntPtr callPtr, VATRPChat chat, char key, bool inCompleteMessage);
		bool ComposeAndSendMessage(VATRPChat chat, string text);
        void MarkChatAsRead(ChatID chat);
        void UpdateRTTFontFamily(string newFont);
        bool HasUnreadMessages();
        void ActivateChat(VATRPChat chat);

        event EventHandler<ConversationEventArgs> ConversationClosed;
        event EventHandler<ConversationEventArgs> ConversationStateChanged;
        event EventHandler<ConversationEventArgs> ConversationUnReadStateChanged;
        event EventHandler<ConversationUpdatedEventArgs> ConversationUpdated;
        event EventHandler<ContactEventArgs> ContactAdded;
        event EventHandler<ContactRemovedEventArgs> ContactRemoved;
        event EventHandler<EventArgs> ContactsChanged;
        event EventHandler<ConversationEventArgs> NewConversationCreated;
        event EventHandler<EventArgs> RttReceived;
        event EventHandler<DeclineMessageArgs> ConversationDeclineMessageReceived;
        
    }
}
