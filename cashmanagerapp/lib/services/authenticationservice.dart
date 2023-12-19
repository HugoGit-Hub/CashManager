import 'package:dio/dio.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';

class AuhtenticationService {
  final Dio _dio = Dio();

  Future<String> register(firstname, lastname, email, password) async {
    const FlutterSecureStorage storage = FlutterSecureStorage();
    try {
      Response response = await _dio
          .post("http://localhost:5001/api/Authentication/Register", data: {
        "firstname": firstname,
        "lastname": lastname,
        "email": email,
        "password": password
      });
      // Check if the request was successful
      if (response.statusCode == 200) {
        // Parse the response data
        String data = response.data;
        storage.write(key: "access_token", value: data);
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

  Future<String> login(email, password) async {
    const FlutterSecureStorage storage = FlutterSecureStorage();
    try {
      Response response = await _dio.post(
          "http://localhost:5001/api/Authentication/Login",
          data: {"email": email, "password": password});
      // Check if the request was successful
      if (response.statusCode == 200) {
        // Parse the response data
        String data = response.data;
        storage.write(key: "access_token", value: data);
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
