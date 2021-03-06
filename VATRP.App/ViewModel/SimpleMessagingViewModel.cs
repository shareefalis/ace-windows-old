using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows.Data;
using System.Windows.Threading;
using com.vtcsecure.ace.windows.Model;
using com.vtcsecure.ace.windows.Services;
using VATRP.Core.Events;
using VATRP.Core.Extensions;
using VATRP.Core.Interfaces;
using VATRP.Core.Model;


namespace com.vtcsecure.ace.windows.ViewModel
{
    public class SimpleMessagingViewModel : MessagingViewModel
    {

        #region Members

        private string _receiverAddress;
        private string _contactSearchCriteria;
        private ICollectionView contactsListView;
        
        #endregion

        #region Events

        public event EventHandler UnreadMessagesCountChanged;
        public event EventHandler<DeclineMessageArgs> DeclineMessageReceived;
        #endregion

        public SimpleMessagingViewModel()
        {
            Init();
        }

        public SimpleMessagingViewModel(IChatService chatMng, IContactsService contactsMng)
            : base(chatMng, contactsMng)
        {
            Init();
            _chatsManager.ConversationUnReadStateChanged += OnUnreadStateChanged;
            _chatsManager.ConversationDeclineMessageReceived += OnDeclineMessageReceived;
        }

        private void OnDeclineMessageReceived(object sender, DeclineMessageArgs args)
        {
            if (ServiceManager.Instance.Dispatcher.Thread != Thread.CurrentThread)
            {
                ServiceManager.Instance.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                    new EventHandler<DeclineMessageArgs>(OnDeclineMessageReceived) , sender, new object[] { args });
                return;
            }

            var newArgs = new DeclineMessageArgs(args.DeclineMessage);
            newArgs.Sender = args.Sender;
            if (DeclineMessageReceived != null) 
                DeclineMessageReceived(sender, newArgs);
        }

        private void OnUnreadStateChanged(object sender, VATRP.Core.Events.ConversationEventArgs e)
        {
            if (ServiceManager.Instance.Dispatcher.Thread != Thread.CurrentThread)
            {
                ServiceManager.Instance.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                    new EventHandler<VATRP.Core.Events.ConversationEventArgs>(OnUnreadStateChanged), sender, new object[] { e });
                return;
            }

            ChangeUnreadCounter();
        }

        private void Init()
        {
            _contactSearchCriteria = string.Empty;
            _receiverAddress = string.Empty;
            LoadContacts();
            this.ContactsListView = CollectionViewSource.GetDefaultView(this.Contacts);
            this.ContactsListView.Filter = new Predicate<object>(this.FilterContactsList);
        }


        #region Methods

        protected override void ChangeUnreadCounter()
        {
            if (UnreadMessagesCountChanged != null)
                UnreadMessagesCountChanged(this, EventArgs.Empty);
        }

        internal void SendMessage(string message)
        {
            if (!message.NotBlank() || (Chat == null && string.IsNullOrEmpty( ReceiverAddress)))
                return;

            _chatsManager.ComposeAndSendMessage(Chat, message);
            MessageText = string.Empty;
        }

        public bool FilterContactsList(object item)
        {
            var contactVM = item as ContactViewModel;
            if (contactVM != null)
                return contactVM.Contact != null && contactVM.Contact.Fullname.Contains(ContactSearchCriteria);
            return true;
        }
		
   
        #endregion

        #region Properties

        public string ReceiverAddress
        {
            get { return _receiverAddress; }
            set
            {
                _receiverAddress = value;
                OnPropertyChanged("ReceiverAddress");
                OnPropertyChanged("ShowReceiverHint");
            }
        }

        public bool ShowReceiverHint
        {
            get { return !ReceiverAddress.NotBlank(); }
        }


        public bool ShowSearchHint
        {
            get { return !ContactSearchCriteria.NotBlank(); }
        }


        public string ContactSearchCriteria
        {
            get { return _contactSearchCriteria; }
            set
            {
                if (_contactSearchCriteria != value)
                {
                    _contactSearchCriteria = value;
                    OnPropertyChanged("ContactSearchCriteria");
                    OnPropertyChanged("ShowSearchHint");
                    ContactsListView.Refresh();
                }
            }
        }

        public ICollectionView ContactsListView
        {
            get { return this.contactsListView; }
            private set
            {
                if (value == this.contactsListView)
                {
                    return;
                }

                this.contactsListView = value;
                OnPropertyChanged("ContactsListView");
            }
        }

       
        #endregion

        internal bool CheckReceiverContact()
        {
            var receiver = string.Empty;
            if (ReceiverAddress != null)
            {
                receiver = ReceiverAddress.Trim();
            }

            if (Contact != null && receiver == Contact.Contact.RegistrationName)
                return true;

            VATRPContact contact = _chatsManager.FindContact(new ContactID(receiver, IntPtr.Zero));
            if (contact == null)
            {
                string un, host, dn;
                int port;
                if (!VATRPCall.ParseSipAddress(receiver, out un,
                    out host, out port))
                    un = "";

                if (!un.NotBlank())
                    return false;

                if (string.IsNullOrEmpty(host))
                    host = App.CurrentAccount.ProxyHostname;
                var contactAddress = string.Format("{0}@{1}", un, host);
                var contactID = new ContactID(contactAddress, IntPtr.Zero);

                contact = new VATRPContact(contactID)
                {
                    DisplayName = un,
                    Fullname = un,
                    SipUsername = un,
                    RegistrationName = contactAddress
                };
                _contactsManager.AddContact(contact, "");
            }

            SetActiveChatContact(contact, IntPtr.Zero);
            if ( ReceiverAddress != contact.RegistrationName )
                ReceiverAddress = contact.RegistrationName;

            return true;
        }
    }
}