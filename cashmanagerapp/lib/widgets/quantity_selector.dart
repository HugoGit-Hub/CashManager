import 'package:flutter/material.dart';

class QuantitySelector extends StatefulWidget {
  @override
  State<QuantitySelector> createState() => _QuantitySelectorState();
}

class _QuantitySelectorState extends State<QuantitySelector> {
  int quantity = 1;

  void incrementQuantity() {
    setState(() {
      quantity = quantity + 1;
    });
  }

  void decrementQuantity() {
    if (quantity > 1) {
      setState(() {
        quantity = quantity - 1;
      });
    }
  }

  @override
  Widget build(BuildContext context) {
    return Row(
      mainAxisAlignment: MainAxisAlignment.spaceBetween,
      children: [
        TextButton(
          onPressed: decrementQuantity,
          child: Text(
            '-',
            style: TextStyle(
              fontWeight: FontWeight.bold,
              fontSize: 16.0,
              color: Colors.black,
            ),
          ),
        ),
        Text(
          quantity.toString(),
          style: TextStyle(
              fontWeight: FontWeight.bold, fontSize: 16.0, color: Colors.black),
        ),
        TextButton(
          onPressed: incrementQuantity,
          child: Text(
            '+',
            style: TextStyle(
                fontWeight: FontWeight.bold,
                fontSize: 16.0,
                color: Colors.black),
          ),
        ),
        SizedBox(width: 10),
      ],
    );
  }
}
