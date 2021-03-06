// ReSharper disable All
using MixERP.Net.DbFactory;
using MixERP.Net.Framework;
using MixERP.Net.Framework.Extensions;
using PetaPoco;
using MixERP.Net.Entities.Transactions;
using Npgsql;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
namespace MixERP.Net.Schemas.Transactions.Data
{
    /// <summary>
    /// Prepares, validates, and executes the function "transactions.get_average_party_transaction(party_id bigint, office_id integer)" on the database.
    /// </summary>
    public class GetAveragePartyTransactionProcedure : DbAccess, IGetAveragePartyTransactionRepository
    {
        /// <summary>
        /// The schema of this PostgreSQL function.
        /// </summary>
        public override string _ObjectNamespace => "transactions";
        /// <summary>
        /// The schema unqualified name of this PostgreSQL function.
        /// </summary>
        public override string _ObjectName => "get_average_party_transaction";
        /// <summary>
        /// Login id of application user accessing this PostgreSQL function.
        /// </summary>
        public long _LoginId { get; set; }
        /// <summary>
        /// User id of application user accessing this table.
        /// </summary>
        public int _UserId { get; set; }
        /// <summary>
        /// The name of the database on which queries are being executed to.
        /// </summary>
        public string _Catalog { get; set; }

        /// <summary>
        /// Maps to "party_id" argument of the function "transactions.get_average_party_transaction".
        /// </summary>
        public long PartyId { get; set; }
        /// <summary>
        /// Maps to "office_id" argument of the function "transactions.get_average_party_transaction".
        /// </summary>
        public int OfficeId { get; set; }

        /// <summary>
        /// Prepares, validates, and executes the function "transactions.get_average_party_transaction(party_id bigint, office_id integer)" on the database.
        /// </summary>
        public GetAveragePartyTransactionProcedure()
        {
        }

        /// <summary>
        /// Prepares, validates, and executes the function "transactions.get_average_party_transaction(party_id bigint, office_id integer)" on the database.
        /// </summary>
        /// <param name="partyId">Enter argument value for "party_id" parameter of the function "transactions.get_average_party_transaction".</param>
        /// <param name="officeId">Enter argument value for "office_id" parameter of the function "transactions.get_average_party_transaction".</param>
        public GetAveragePartyTransactionProcedure(long partyId, int officeId)
        {
            this.PartyId = partyId;
            this.OfficeId = officeId;
        }
        /// <summary>
        /// Prepares and executes the function "transactions.get_average_party_transaction".
        /// </summary>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
        public decimal Execute()
        {
            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Execute, this._LoginId, this._Catalog, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to the function \"GetAveragePartyTransactionProcedure\" was denied to the user with Login ID {LoginId}.", this._LoginId);
                    throw new UnauthorizedException("Access is denied.");
                }
            }
            string query = "SELECT * FROM transactions.get_average_party_transaction(@PartyId, @OfficeId);";

            query = query.ReplaceWholeWord("@PartyId", "@0::bigint");
            query = query.ReplaceWholeWord("@OfficeId", "@1::integer");


            List<object> parameters = new List<object>();
            parameters.Add(this.PartyId);
            parameters.Add(this.OfficeId);

            return Factory.Get<decimal>(this._Catalog, query, parameters.ToArray()).FirstOrDefault();
        }


    }
}