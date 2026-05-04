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
          _OceanDetailHeroCard(
            coverUrl: args.coverUrl,
            title: args.title,
            subtitle: args.subtitle,
            description: args.content,
            rating: _rating,
            reviewText: '(${_favoriteCount ~/ 2} reviews)',
            metricOne: '31 Mar',
            metricTwo: '$_favoriteCount likes',
            actionText: 'Theo dõi',
          ),
          const SizedBox(height: 10),
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

class _OceanDetailHeroCard extends StatelessWidget {
  const _OceanDetailHeroCard({
    required this.coverUrl,
    required this.title,
    required this.subtitle,
    required this.description,
    required this.rating,
    required this.reviewText,
    required this.metricOne,
    required this.metricTwo,
    required this.actionText,
  });

  final String coverUrl;
  final String title;
  final String subtitle;
  final String description;
  final double rating;
  final String reviewText;
  final String metricOne;
  final String metricTwo;
  final String actionText;

  @override
  Widget build(BuildContext context) {
    return Container(
      decoration: BoxDecoration(
        borderRadius: BorderRadius.circular(24),
        boxShadow: <BoxShadow>[
          BoxShadow(
            color: Colors.black.withValues(alpha: 0.08),
            blurRadius: 18,
            offset: const Offset(0, 8),
          ),
        ],
      ),
      child: ClipRRect(
        borderRadius: BorderRadius.circular(24),
        child: Container(
          color: Colors.white,
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: <Widget>[
              Stack(
                children: <Widget>[
                  Image.network(
                    coverUrl,
                    height: 300,
                    width: double.infinity,
                    fit: BoxFit.cover,
                  ),
                  Positioned(
                    top: 14,
                    right: 14,
                    child: Container(
                      decoration: BoxDecoration(
                        color: Colors.white.withValues(alpha: 0.28),
                        borderRadius: BorderRadius.circular(12),
                      ),
                      child: const Padding(
                        padding: EdgeInsets.all(8),
                        child: Icon(Icons.bookmark_border_rounded, color: Colors.white),
                      ),
                    ),
                  ),
                ],
              ),
              Container(
                width: double.infinity,
                padding: const EdgeInsets.fromLTRB(18, 16, 18, 18),
                decoration: const BoxDecoration(
                  color: Colors.white,
                  borderRadius: BorderRadius.vertical(top: Radius.circular(24)),
                ),
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: <Widget>[
                    Text(
                      title,
                      style: const TextStyle(
                        fontSize: 30,
                        fontWeight: FontWeight.w800,
                        color: Color(0xFF003459),
                        height: 1.06,
                      ),
                    ),
                    const SizedBox(height: 6),
                    Text(
                      subtitle,
                      style: const TextStyle(
                        color: Color(0xFF00A5CF),
                        fontWeight: FontWeight.w600,
                      ),
                    ),
                    const SizedBox(height: 10),
                    Row(
                      children: <Widget>[
                        Text(
                          reviewText,
                          style: const TextStyle(color: Color(0xFF90A4AE), fontSize: 13),
                        ),
                        const SizedBox(width: 8),
                        Text(
                          '${rating.toStringAsFixed(1)} ★',
                          style: const TextStyle(fontWeight: FontWeight.w700),
                        ),
                      ],
                    ),
                    const SizedBox(height: 10),
                    Text(
                      description,
                      maxLines: 4,
                      overflow: TextOverflow.ellipsis,
                      style: const TextStyle(
                        height: 1.45,
                        color: Color(0xFF607D8B),
                      ),
                    ),
                    const SizedBox(height: 12),
                    Row(
                      children: <Widget>[
                        const Icon(Icons.schedule_rounded, size: 16, color: Color(0xFF0A3D62)),
                        const SizedBox(width: 4),
                        Text(metricOne),
                        const SizedBox(width: 12),
                        const Icon(Icons.insights_rounded, size: 16, color: Color(0xFF0A3D62)),
                        const SizedBox(width: 4),
                        Text(metricTwo),
                      ],
                    ),
                    const SizedBox(height: 14),
                    Row(
                      children: <Widget>[
                        const Text(
                          '\$1465',
                          style: TextStyle(fontWeight: FontWeight.w800, fontSize: 24),
                        ),
                        const SizedBox(width: 8),
                        const Text('/ 20 hrs', style: TextStyle(color: Color(0xFF90A4AE))),
                        const Spacer(),
                        FilledButton(
                          onPressed: () {},
                          style: FilledButton.styleFrom(
                            backgroundColor: const Color(0xFF4CC9F0),
                            shape: RoundedRectangleBorder(
                              borderRadius: BorderRadius.circular(14),
                            ),
                            padding: const EdgeInsets.symmetric(horizontal: 20, vertical: 12),
                          ),
                          child: Text(actionText),
                        ),
                      ],
                    ),
                  ],
                ),
              ),
            ],
          ),
        ),
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
