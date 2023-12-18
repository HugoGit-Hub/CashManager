import 'package:flutter/material.dart';

class ButtonAddToCart extends StatelessWidget {
  final double totalPrice;
  const ButtonAddToCart({Key? key, required this.totalPrice}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Center(
      child: ElevatedButton(
        onPressed: () {},
        style: ElevatedButton.styleFrom(
          padding: EdgeInsets.only(
            top: 15.0,
            bottom: 15.0,
            left: 40.0,
            right: 40.0,
          ),
          shape: RoundedRectangleBorder(
            borderRadius: BorderRadius.circular(50),
            side: BorderSide(
              color: Colors.yellow[600]!,
              width: 1.0,
            ),
          ),
        ),
        child: Column(
          children: [
            Text(
              'Ajouter au panier',
              style: TextStyle(
                color: Colors.black,
                fontSize: 12.0,
              ),
            ),
            Text(
              '$totalPrice €',
              style: TextStyle(
                color: Colors.black,
                fontWeight: FontWeight.bold,
                fontSize: 12.0,
              ),
            ),
          ],
        ),
      ),
    );
  }
}
