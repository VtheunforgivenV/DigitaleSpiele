public interface ILootableObject {

    Inventory getItem();
    bool isLooted();
    void setLooted(bool looted);
    bool isOpened();
    void setOpened(bool opened);
}
