using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DaoConversion.DaoToEntity.Abstractions;

public interface IDaoToEntity<in TDao, out TEntity>
{
    TEntity Convert(TDao dao);
}
