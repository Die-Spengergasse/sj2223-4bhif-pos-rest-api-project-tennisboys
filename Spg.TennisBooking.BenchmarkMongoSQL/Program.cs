using Spg.TennisBooking.BenchmarkMongoSQL;

//Benchmark SQL
BenchmarkSQL benchmarkSQL = new();
benchmarkSQL.Benching();

//Sleep 5 sec
Console.WriteLine("Sleep 5 sec");
Thread.Sleep(5000);

//Benchmark Mongo
//BenchmarkMongo benchmarkMongo = new();
//benchmarkMongo.Benching();