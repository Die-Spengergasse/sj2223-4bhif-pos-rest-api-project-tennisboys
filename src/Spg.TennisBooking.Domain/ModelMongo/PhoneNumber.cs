using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Spg.TennisBooking.Domain.ModelMongo
{
    public record PhoneNumber(string Prefix, string Number)
    {
        
     }
}

