import 'package:flutter/material.dart';

class ButtonPlaceOrder extends StatelessWidget {
  const ButtonPlaceOrder({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Center(
      child: ElevatedButton(
        onPressed: () {
          Navigator.of(context).pushNamed('/paiement');
        },
        style: ElevatedButton.styleFrom(
          backgroundColor: Colors.yellow[600]!,
          foregroundColor: Colors.white,
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
          elevation: 4,
        ),
        child: Column(
          children: [
            Text(
              'Payer'.toUpperCase(),
              style: TextStyle(
                color: Colors.black,
                fontSize: 12.0,
              ),
            ),
          ],
        ),
      ),
    );
  }
}
