import 'package:flutter/material.dart';

class NewsDetailArgs {
  const NewsDetailArgs({
    required this.title,
    required this.subtitle,
    required this.coverUrl,
    required this.thumbnailUrl,
    required this.content,
    required this.initialRating,
    required this.initialFavoriteCount,
    required this.initialComments,
  });

  final String title;
  final String subtitle;
  final String coverUrl;
  final String thumbnailUrl;
  final String content;
  final double initialRating;
  final int initialFavoriteCount;
  final List<String> initialComments;
}

class NewsDetailPage extends StatefulWidget {
  const NewsDetailPage({super.key});

  static const String routeName = '/news-detail';

  @override
  State<NewsDetailPage> createState() => _NewsDetailPageState();
}

class _NewsDetailPageState extends State<NewsDetailPage> {
  final TextEditingController _commentController = TextEditingController();
  late List<String> _comments;
  late double _rating;
  late int _favoriteCount;
  bool _isFavorite = false;

  @override
  void didChangeDependencies() {
    super.didChangeDependencies();
    final args = ModalRoute.of(context)!.settings.arguments! as NewsDetailArgs;
    _comments = List<String>.from(args.initialComments);
    _rating = args.initialRating;
    _favoriteCount = args.initialFavoriteCount;
  }

  @override
  void dispose() {
    _commentController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    final args = ModalRoute.of(context)!.settings.arguments! as NewsDetailArgs;
    return Scaffold(
      appBar: AppBar(title: const Text('Chi tiết tin tức')),
      body: ListView(
        padding: const EdgeInsets.all(16),
        children: <Widget>[
          ClipRRect(
            borderRadius: BorderRadius.circular(16),
            child: Image.network(args.coverUrl, height: 220, fit: BoxFit.cover),
          ),
          const SizedBox(height: 14),
          Row(
            children: <Widget>[
              ClipRRect(
                borderRadius: BorderRadius.circular(10),
                child: Image.network(args.thumbnailUrl, width: 64, height: 64, fit: BoxFit.cover),
              ),
              const SizedBox(width: 10),
              Expanded(
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: <Widget>[
                    Text(args.title, style: const TextStyle(fontSize: 18, fontWeight: FontWeight.w700)),
                    const SizedBox(height: 4),
                    Text(args.subtitle, style: const TextStyle(fontSize: 13, color: Color(0xFF546E7A))),
                  ],
                ),
              ),
            ],
          ),
          const SizedBox(height: 14),
          Text(args.content, style: const TextStyle(height: 1.45)),
          const SizedBox(height: 18),
          _SocialBar(
            isFavorite: _isFavorite,
            favoriteCount: _favoriteCount,
            rating: _rating,
            onFavorite: () {
              setState(() {
                _isFavorite = !_isFavorite;
                _favoriteCount += _isFavorite ? 1 : -1;
              });
            },
            onRate: (double value) => setState(() => _rating = value),
          ),
          const SizedBox(height: 18),
          const Text('Bình luận', style: TextStyle(fontSize: 16, fontWeight: FontWeight.w700)),
          const SizedBox(height: 8),
          ..._comments.map(
            (String comment) => Container(
              margin: const EdgeInsets.only(bottom: 8),
              padding: const EdgeInsets.all(12),
              decoration: BoxDecoration(
                color: Colors.white,
                borderRadius: BorderRadius.circular(10),
                boxShadow: <BoxShadow>[
                  BoxShadow(
                    color: Colors.black.withValues(alpha: 0.04),
                    blurRadius: 8,
                    offset: const Offset(0, 3),
                  ),
                ],
              ),
              child: Text(comment),
            ),
          ),
          const SizedBox(height: 10),
          TextField(
            controller: _commentController,
            decoration: InputDecoration(
              hintText: 'Viết bình luận...',
              suffixIcon: IconButton(
                onPressed: () {
                  final value = _commentController.text.trim();
                  if (value.isEmpty) return;
                  setState(() => _comments.insert(0, value));
                  _commentController.clear();
                },
                icon: const Icon(Icons.send_rounded),
              ),
            ),
          ),
        ],
      ),
    );
  }
}

class _SocialBar extends StatelessWidget {
  const _SocialBar({
    required this.isFavorite,
    required this.favoriteCount,
    required this.rating,
    required this.onFavorite,
    required this.onRate,
  });

  final bool isFavorite;
  final int favoriteCount;
  final double rating;
  final VoidCallback onFavorite;
  final ValueChanged<double> onRate;

  @override
  Widget build(BuildContext context) {
    return Row(
      children: <Widget>[
        IconButton(
          onPressed: onFavorite,
          icon: Icon(
            isFavorite ? Icons.favorite_rounded : Icons.favorite_border_rounded,
            color: isFavorite ? Colors.redAccent : const Color(0xFF78909C),
          ),
        ),
        Text('$favoriteCount'),
        const SizedBox(width: 16),
        const Icon(Icons.star_rounded, color: Color(0xFFFFB300)),
        const SizedBox(width: 4),
        Text(rating.toStringAsFixed(1)),
        const Spacer(),
        PopupMenuButton<double>(
          onSelected: onRate,
          itemBuilder: (BuildContext context) => List<PopupMenuEntry<double>>.generate(
            5,
            (int index) {
              final value = index + 1.0;
              return PopupMenuItem<double>(
                value: value,
                child: Text('Đánh giá $value sao'),
              );
            },
          ),
          child: const Padding(
            padding: EdgeInsets.symmetric(horizontal: 10, vertical: 8),
            child: Row(
              children: <Widget>[
                Icon(Icons.rate_review_rounded, size: 18),
                SizedBox(width: 4),
                Text('Đánh giá'),
              ],
            ),
          ),
        ),
      ],
    );
  }
}
