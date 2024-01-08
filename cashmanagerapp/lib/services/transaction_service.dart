import 'package:dio/dio.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';

class TransactionService {
  Future<void> createTransaction(String creditor , int methodPaiement) async {
    const FlutterSecureStorage storage = FlutterSecureStorage();
    final String? token = await storage.read(key: 'access_token');

    try {
      await Dio().post(
        'https://g24.epihub.eu:444/api/Transaction/Create',
        data: {
          'creditor': creditor,
          'method' : methodPaiement
        },
        options: Options(
          headers: {
            "Authorization": "Bearer $token",
          },
        ),
      );
    } catch (e) {
      print(e);
    }
  }
}