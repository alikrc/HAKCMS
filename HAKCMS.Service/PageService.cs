using HAKCMS.Data.Infrastructure;
using HAKCMS.Data.Repository;
using HAKCMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HAKCMS.Service
{
    public interface IPageService
    {
        IEnumerable<TBL_Page> GetAll();
        TBL_Page Get(int id);
        void Create(TBL_Page icerik);
        void Delete(int id);
        void Save();
    }

    public class PageService : IPageService
    {
        private readonly IPageRepository icerikRepository;
        private readonly IUnitOfWork unitOfWork;

        public PageService(IPageRepository icerikRepository, IUnitOfWork unitOfWork)
        {
            this.icerikRepository = icerikRepository;
            this.unitOfWork = unitOfWork;
        }

        #region IIcerikService Members

        public IEnumerable<TBL_Page> GetAll()
        {
            var icerikler = icerikRepository.GetAll();
            return icerikler;
        }

        public TBL_Page Get(int id)
        {
            var icerik = icerikRepository.GetById(id);
            return icerik;
        }

        public void Create(TBL_Page icerik)
        {
            icerikRepository.Add(icerik);
            Save();
        }

        public void Delete(int id)
        {
            var icerik = icerikRepository.GetById(id);
            icerikRepository.Add(icerik);
            Save();
        }

        public void Save()
        {
            unitOfWork.Commit();
        }

        #endregion
    }

}
