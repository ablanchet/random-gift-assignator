using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace RandomGiftAssignator
{
    internal class Participant
    {
        private Participant _lovedOne;

        public string Firstname { get; set; }

        public string Email { get; set; }

        public Participant LovedOne
        {
            get
            {
                return _lovedOne;
            }
            set
            {
                _lovedOne = value;
                // Interplay relation. If A is in love with B so B is in love with A
                if( value.LovedOne == null || value.LovedOne != this )
                    value.LovedOne = this;
            }
        }

        public Participant GiftedParticipant { get; set; }

        internal bool CanGift( Participant participantToGift )
        {
            return participantToGift != this && participantToGift != this.LovedOne;
        }

        internal void SendEmail()
        {
            string message;
            using (var rdr = File.OpenText("mail.html"))
                message = string.Format(rdr.ReadToEnd(), GiftedParticipant.Firstname);

            var mailMessage = new MailMessage( "sender@domain.local", Email, "Le cadeau à faire jeudi !", message ) { IsBodyHtml = true };

            using( var smtpClient = new SmtpClient() )
                smtpClient.Send( mailMessage );
        }

        public override string ToString()
        {
            return string.Format( "{0} should offer a gift to {1}", Firstname, GiftedParticipant.Firstname );
        }
    }
}
