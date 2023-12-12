import 'package:flutter/material.dart';

class ButtonLike extends StatelessWidget {
  const ButtonLike({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Center(
      child: ElevatedButton(
        onPressed: () {},
        style: ElevatedButton.styleFrom(
          backgroundColor: Color(0xFF12B76A),
          padding: EdgeInsets.only(
            top: 15.0,
            bottom: 15.0,
            left: 30.0,
            right: 30.0,
          ),
          shape: RoundedRectangleBorder(
            borderRadius: BorderRadius.circular(50),
          ),
        ),
        child: const Icon(
          Icons.favorite,
          color: Colors.white,
          size: 24.0,
        ),
      ),
    );
  }
}
