using System;

namespace OrderBot
{
    public class Session
    {
        private enum State
        {
            WELCOMING, TRAVEL_DATE, TRAVEL_TIME, TRAVEL_PLACE, TRAVEL_CLASS
        }

        private State nCur = State.WELCOMING;
        private Order oOrder;

        public Session(string sPhone)
        {
            this.oOrder = new Order();
            this.oOrder.Phone = sPhone;
        }

        public List<String> OnMessage(String sInMessage)
        {
            List<String> aMessages = new List<string>();
            switch (this.nCur)
            {
                case State.WELCOMING:
                    aMessages.Add("Welcome to Airline Ticket Booking System");
                    aMessages.Add("How may we (Group 4) help you?");
                    aMessages.Add("For which dates you want to book the ticket");
                    this.nCur = State.TRAVEL_DATE;
                    break;

                case State.TRAVEL_DATE:
                    this.oOrder.TravelDate = sInMessage;
                    this.oOrder.Save();
                    aMessages.Add("At what time would you like to travel "  );
                    this.nCur = State.TRAVEL_TIME;
                    break;
                
                case State.TRAVEL_TIME:
                    this.oOrder.TravelTime = sInMessage;
                    this.oOrder.Save();
                    aMessages.Add("What is the name of your destination "  );
                    this.nCur = State.TRAVEL_PLACE;
                    break;

                case State.TRAVEL_PLACE:
                    this.oOrder.TravelPlace = sInMessage;
                    this.oOrder.Save();
                    aMessages.Add("In which class would you like to travel (1.Business 2.Economy) " );
                    this.nCur = State.TRAVEL_CLASS;
                    break;
                
                case State.TRAVEL_CLASS:
                    //string sProtein = sInMessage;
                    this.oOrder.TravelClass = sInMessage;
                    this.oOrder.Save();
                    aMessages.Add("Thankyou to booking your trip");
                    this.nCur = State.WELCOMING;
                    break;


            }
            aMessages.ForEach(delegate (String sMessage)
            {
                System.Diagnostics.Debug.WriteLine(sMessage);
            });
            return aMessages;
        }

    }
}
