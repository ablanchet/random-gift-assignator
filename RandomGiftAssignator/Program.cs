using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RandomGiftAssignator
{
    class Program
    {
        private static void Main( string[] args )
        {
            // sets the participants to the gift party
            var participants = new Dictionary<string, Participant>()
            {
                {"Participant 1", new Participant {Firstname = "P1", Email = "p1@participant.local"}},
                {"Participant 2", new Participant {Firstname = "P2", Email = "p2@participant.local"}},
                {"Participant 3", new Participant {Firstname = "P3", Email = "p3@participant.local"}},
                {"Participant 4", new Participant {Firstname = "P4", Email = "p4@participant.local"}},
            };

            // sets the loved ones
            participants["Participant 1"].LovedOne = participants["Participant 2"];
            
            var assignator = new GiftAssignator( participants.Values );
            foreach( var participant in assignator.RandomAssign() )
            {
                Console.WriteLine( "{0} -> {1}", participant.Firstname, participant.GiftedParticipant.Firstname );
                try
                {
                    participant.SendEmail();
                }
                catch( Exception ex )
                {
                    Console.WriteLine( "Error : {0}", ex );
                }
                System.Threading.Thread.Sleep( 500 );
            }

            Console.ReadLine();
        }
    }
}
