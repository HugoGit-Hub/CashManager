import 'package:flutter/material.dart';
class AppLargeText extends StatelessWidget {
  final double size=16;
  final String text;
  final Color color;
   AppLargeText({Key? key, 
   required this.text,
   this.color = Colors.black}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Text(
      text,
      style: TextStyle(
        fontSize: size,
        color: color,
        fontWeight: FontWeight.bold,
        ),
    );
  }
}