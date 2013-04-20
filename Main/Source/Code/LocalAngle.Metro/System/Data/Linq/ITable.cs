using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.Linq
{
    // Summary:
    //     Used for weakly typed query scenarios.
    public interface ITable
    {
        // Summary:
        //     Gets the System.Data.Linq.DataContext that has been used to retrieve this
        //     System.Data.Linq.ITable.
        //
        // Returns:
        //     The System.Data.Linq.DataContext used to retrieve the System.Data.Linq.ITable.
        DataContext Context { get; }
        //
        // Summary:
        //     Indicates if the type of the entities contained in this System.Data.Linq.ITable
        //     instance has a primary key.
        //
        // Returns:
        //     Returns true if the entity type does not have a primary key; otherwise, false.
        bool IsReadOnly { get; }
    }

    // Summary:
    //     Represents a table for a particular type in the underlying database.
    //
    // Type parameters:
    //   TEntity:
    //     The type of the data in the table.
    public interface ITable<TEntity> where TEntity : class
    {
        // Summary:
        //     When overridden, attaches a disconnected or "detached" entity to a new System.Data.Linq.DataContext
        //     when original values are required for optimistic concurrency checks.
        //
        // Parameters:
        //   entity:
        //     The object to be added.
        void Attach(TEntity entity);
        //
        // Summary:
        //     When overridden, puts an entity from this table into a pending delete state.
        //
        // Parameters:
        //   entity:
        //     The object to delete.
        void DeleteOnSubmit(TEntity entity);
        //
        // Summary:
        //     When overridden, adds an entity in a pending insert state to this System.Data.Linq.ITable<TEntity>.
        //
        // Parameters:
        //   entity:
        //     The object to insert.
        void InsertOnSubmit(TEntity entity);
    }
}
