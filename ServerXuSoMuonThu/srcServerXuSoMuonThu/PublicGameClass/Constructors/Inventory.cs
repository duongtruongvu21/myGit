namespace PublicGameClass.Constructors
{
    [System.Serializable]
    public class InventoryItem
    {
        public int itemID;
        public int SoLuong;

        public InventoryItem(int itemID, int soLuong)
        {
            this.itemID=itemID;
            SoLuong=soLuong;
        }
    }
}
