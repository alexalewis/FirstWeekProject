using System;
using System.Collections.Generic;

namespace FirstWeekProject
{
  class Program
  {
    //method for player/dealer total
    public static int Total(List<Card> hand)
    {
      var total = 0;
      for (int i = 0; i < hand.Count; i++)
      {
        total += hand[i].GetCardValue();
      }
      return total;
    }
    public static string CardList(List<Card> cards)
    {
      var cardList = "";
      for (var i = 0; i < cards.Count; i++)
      {
        cardList += cards[i].DisplayCard();
      }
      return cardList;
    }


    static void Main(string[] args)
    {
      Console.WriteLine("Welcome to BlackJack");

      //Creating variables
      var fullDeck = new List<Card>();
      var deckSuit = new List<string>() { "Hearts", "Diamonds", "Spades", "Clubs" };
      var deckFace = new List<string>() { "ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "jack", "queen", "king" };
      var aCard = "";
      var playAgain = true;

      while (playAgain)
      {
        //Creating deck
        for (int i = 0; i < deckSuit.Count; i++)
        {
          for (int j = 0; j < deckFace.Count; j++)
          {
            var card = new Card();
            card.Rank = deckFace[j];
            card.Suit = deckSuit[i];
            if (card.Suit == "diamonds" || card.Suit == "hearts")
            {
              card.ColorOfTheCard = "red";
            }
            else
            {
              card.ColorOfTheCard = "black";
            }
            fullDeck.Add(card);
            aCard = ($"{deckFace[j]} of {deckSuit[i]}");
          }
        }


        //Shuffling deck
        Random rnd = new Random();

        //for i from n - 1 down to 1 do:
        for (int i = 52 - 1; i >= 1; i--)
        {
          //j = random integer (where 0 <= j <= i)
          int j = rnd.Next(i);
          //swap fullDeck[i] with fullDeck[j]
          var temp = fullDeck[j];
          fullDeck[j] = fullDeck[i];
          fullDeck[i] = temp;
        }

        //Dealing cards

        var dealerHand = new List<Card>();

        // add to dealer hand
        dealerHand.Add(fullDeck[0]);
        dealerHand.Add(fullDeck[1]);
        //remove from deck
        fullDeck.RemoveAt(0);
        fullDeck.RemoveAt(0);


        var playerHand = new List<Card>();

        // add to player hand
        playerHand.Add(fullDeck[0]);
        playerHand.Add(fullDeck[1]);
        //remove from deck
        fullDeck.RemoveAt(0);
        fullDeck.RemoveAt(0);

        var dealerTotal = Total(dealerHand);
        var playerTotal = Total(playerHand);
        var dealerList = CardList(dealerHand);
        var playerList = CardList(playerHand);
        var isPlaying = true;
        var userInput = "";
        Console.WriteLine($"Your cards are {playerList} and your current total is {playerTotal}");

        //player conditions
        while (isPlaying && playerTotal <= 20)
        {
          Console.WriteLine("Do you want to (Hit) or (Stay)?");
          userInput = Console.ReadLine().ToLower();

          if (userInput == "hit" && playerTotal <= 20)
          {
            playerHand.Add(fullDeck[0]);
            fullDeck.RemoveAt(0);

            playerTotal = Total(playerHand);
            dealerTotal = Total(dealerHand);
            dealerList = CardList(dealerHand);
            playerList = CardList(playerHand);

            Console.WriteLine($"Your cards are {playerList} and your current total is {playerTotal}");

          }

          if (playerTotal > 21)
          {
            Console.WriteLine("You bust!");
            isPlaying = false;
          }

          //dealer conditions
          while (isPlaying && userInput == "stay")
          {

            if (dealerTotal < 17)
            {
              dealerHand.Add(fullDeck[0]);
              fullDeck.RemoveAt(0);
              dealerTotal = Total(dealerHand);
            }
            if (dealerTotal >= 17 && dealerTotal < 21 && dealerTotal > playerTotal)
            {
              Console.WriteLine($"You lose! The dealers total is {dealerTotal}");
              isPlaying = false;
            }

            else if (dealerTotal >= 17 && dealerTotal < 21 && dealerTotal < playerTotal)
            {
              Console.WriteLine($"You win! The dealers total is {dealerTotal}");
              isPlaying = false;
            }
            if (dealerTotal == 21)
            {
              Console.WriteLine("Dealer got BlackJack! Dealer wins!");
              isPlaying = false;
            }
            if (dealerTotal > 21)
            {
              Console.WriteLine("The dealer busted!");
              isPlaying = false;
            }
            if (dealerTotal == playerTotal)
            {
              Console.WriteLine("Dealer wins!");
              isPlaying = false;
            }
            if (playerTotal == 21 && playerTotal > dealerTotal)
            {
              Console.WriteLine("BlackJack! You win!");
              isPlaying = false;
            }
          }
        }

        Console.WriteLine("Do you want to play again? (Yes) or (No)");
        var playAgainInput = Console.ReadLine().ToLower();
        if (playAgainInput == "yes")
        {
          playAgain = true;

        }
        else if (playAgainInput == "no")
        {
          playAgain = false;
        }
      }


    }
  }


}





