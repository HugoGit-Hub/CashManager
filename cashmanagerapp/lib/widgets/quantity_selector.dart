import 'package:flutter/material.dart';

class QuantitySelector extends StatefulWidget {
  final double price;
  final int quantity;
  final Function(int quantity, double total) onQuantityChanged;
  const QuantitySelector({
    Key? key,
    required this.quantity,
    required this.price,
    required this.onQuantityChanged,
  }) : super(key: key);
  @override
  State<QuantitySelector> createState() => _QuantitySelectorState();
}

class _QuantitySelectorState extends State<QuantitySelector> {
  late int quantity;

  @override
  void initState() {
    super.initState();
    quantity = widget.quantity;
  }

  void incrementQuantity() {
    setState(() {
      quantity = quantity + 1;
      widget.onQuantityChanged(quantity, quantity * widget.price);
    });
  }

  void decrementQuantity() {
    if (quantity > 1) {
      setState(() {
        quantity = quantity - 1;
        widget.onQuantityChanged(quantity, quantity * widget.price);
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
