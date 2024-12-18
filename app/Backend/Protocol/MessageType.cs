namespace Backend.Protocol;

public enum MessageType
{
    HELLO,                      //annonce de départ
    GOOD_BYE,                   //annonce d’absence
    TIME_SYNC,                  //envoie une référence de temps
    TOWN_ENVIRONMENT,           //Infos sur l’environnement actuel
    HOUSE_STATUS,               //Données de monitoring en réponse
    HOUSE_STATUS_REQUEST,       //Demande de status de la part de PowerWatch
    MEDIA_STATUS,
    MEDIA_STATUS_REQUEST,
    CASH,                		//Porteur d'une transaction de cash (CashTransaction)
    POWER                       //Porteur d'une transaction d'énergie (PowerTransaction)
}
