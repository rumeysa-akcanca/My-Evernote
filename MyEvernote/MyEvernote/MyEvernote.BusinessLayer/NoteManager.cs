using MyEvernote.DataAccessLayer.EntityFramework;
using MyEvernote.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.BusinessLayer
{
  public  class NoteManager
    {
        private Repository<Note> repo_note = new Repository<Note>();
        public List<Note> GetAllNote()
        {
            return repo_note.List();
        }

        public IQueryable<Note> GetAllNoteQueryable()
        {
            return repo_note.ListQueryable();
        }
    }
}
