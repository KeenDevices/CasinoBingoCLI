


namespace CasinoBingoCLI{
    
    public class searchInCard{

        public searchInCard(){


        }
        public hasNumber(BingoCard card, int bingoBall){
            for(int j = 0; j<card.GetLength(1);j++){
                for (int i = 0; i < card.GetLength(0); i++)
                {
                    if(card[j,i] == bingoBall){
                        card[i,j] == 0;
                        

                    }

            }



        }






    }




}