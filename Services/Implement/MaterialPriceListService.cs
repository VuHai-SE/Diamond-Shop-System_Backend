using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Repositories;
using Repositories.Implement;

namespace Services.Implement
{
    public class MaterialPriceListService : IMaterialPriceListService
    {
        private readonly IMaterialPriceListRepository materialPriceListRepository;

        public MaterialPriceListService()
        {
            if (materialPriceListRepository == null)
            {
                materialPriceListRepository = new MaterialPriceListRepository();
            }
        }

        public TblMaterialPriceList AddMaterialPriceList(TblMaterialPriceList materialPriceList)
            => materialPriceListRepository.AddMaterialPriceList(materialPriceList);

        public bool DeleteMaterialPriceList(int id)
            => materialPriceListRepository.DeleteMaterialPriceList(id);

        public TblMaterialPriceList GetMaterialPriceList(int id)
            => materialPriceListRepository.GetMaterialPriceList(id);

        public List<TblMaterialPriceList> GetMaterialPriceLists()
            => materialPriceListRepository.GetMaterialPriceLists();

        public bool UpdateMaterialPriceList(int id, TblMaterialPriceList materialPriceList)
            => materialPriceListRepository.UpdateMaterialPriceList(id, materialPriceList);
    }
}
