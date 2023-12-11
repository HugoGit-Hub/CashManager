import 'package:flutter/material.dart';
class AppText extends StatelessWidget {
  final double size = 12;
  final String text;
  final Color color;
   AppText({Key? key, 
   required this.text,
   this.color = Colors.black}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Text(
      text,
      style: TextStyle(
        fontSize: size,
        color: color,
        ),
    );
  }
}