class ShoppingSessionModel {
  double totalPrice;

  ShoppingSessionModel({required this.totalPrice});

  ShoppingSessionModel.fromJson(Map<String, dynamic> json)
      : totalPrice = (json['totalPrice'] as num).toDouble();

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = {};
    data['totalPrice'] = totalPrice.toDouble();
    return data;
  }
}
