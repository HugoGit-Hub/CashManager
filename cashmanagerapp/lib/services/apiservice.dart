import 'package:dio/dio.dart';

class Apiservice {
  final Dio _dio = Dio();

  Future<Map<String, dynamic>> login(name,password) async {
    try {
      Response response = await _dio.post("http://localhost:5001/api/Authentication/register", data: {"name": name,"password": password}) ;
      // Check if the request was successful
      if (response.statusCode == 200) {
        // Parse the response data
        Map<String, dynamic> data = response.data;
        return data;
      } else {
        // Handle error if the request was not successful
        throw Exception('Failed to load data');
      }
    } catch (e) {
      // Handle Dio errors
      print('Error: $e');
      rethrow;
    }
  }
}
