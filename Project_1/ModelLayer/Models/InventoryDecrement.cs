using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLayer.Models
{
    public interface InventoryDecrement
    {
        void PremadeItemGrabed(Item it);
        void ItemWasGrabed(Item item);
        void TooManyGrabed(Item item, int a);
    }
}
