import 'package:dio/dio.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';

class TransactionService {
  Future<void> createTransaction(String creditor , int methodPaiement) async {
    const FlutterSecureStorage storage = FlutterSecureStorage();
    final String? token = await storage.read(key: 'access_token');

    try {
      await Dio().post(
        'https://vh71wppn-5001.uks1.devtunnels.ms/api/Transaction/Create',
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