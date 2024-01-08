import 'package:dio/dio.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:cashmanagerapp/models/shopping_session_model.dart';

class ShoppingSessionService {
  Future<ShoppingSessionModel> getTotalPrice() async {
    const FlutterSecureStorage storage = FlutterSecureStorage();
    final String? token = await storage.read(key: 'access_token');
    try {
      final res = await Dio()
          .get(
        'https://vh71wppn-5001.uks1.devtunnels.ms/api/ShoppingSession/GetCurrentShoppingSession',
        options: Options(
          headers: {
            "Authorization": "Bearer $token",
          },
        ),
      )
          .onError((DioError error, stackTrace) async {
        return Response(
          requestOptions: RequestOptions(path: ''),
          statusCode: error.response?.statusCode ?? 0,
          data: error.response?.data ?? {},
        );
      });
      if (res.statusCode != 200) {
        return ShoppingSessionModel(totalPrice: 0.0);
      }
      return ShoppingSessionModel.fromJson(res.data);
    } catch (e) {
      print(e);
      return ShoppingSessionModel(totalPrice: 0.0);
    }
  }
}
