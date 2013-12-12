using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RandomGiftAssignator
{
    internal class GiftAssignator
    {
        private readonly IEnumerable<Participant> _participants;

        public GiftAssignator( IEnumerable<Participant> participants )
        {
            _participants = participants;
        }

        public IEnumerable<Participant> RandomAssign()
        {
            var random = new Random( (int)DateTime.UtcNow.Ticks );

            var assignedParticipants = new List<Participant>(_participants.Count());
            bool retry = true;

            while( retry )
            {
                retry = false;
                
                assignedParticipants.Clear();

                var participantToAssign = _participants.OrderBy( x => Guid.NewGuid() ).ToList();
                var participantToProcess = _participants.OrderBy( x => Guid.NewGuid() ).ToList();

                foreach( var p in participantToProcess )
                {
                    var gifted = p;
                    while( !p.CanGift( gifted ) )
                    {
                        if( participantToAssign.Count == 1 && !p.CanGift( participantToAssign[0] ) )
                        {
                            retry = true;
                            break;
                        }
                        gifted = participantToAssign[random.Next( 0, participantToAssign.Count )];
                    }
                    if( retry ) break;

                    p.GiftedParticipant = gifted;
                    participantToAssign.Remove( gifted );

                    assignedParticipants.Add( p );
                }
            }

            return assignedParticipants;
        }
    }
}
